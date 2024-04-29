using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using wpf_app.Contracts;
using wpf_app.Models;
using wpf_app.Models.DTOs;

namespace wpf_app.Service;

public class Repository(IConfigurationBuilder configurationBuilder) : DapperBase(configurationBuilder), IRepository
{
    public async Task<IEnumerable<AbonentDto>> GetAbonents(int pageNumber, int count = 15)
    {
        using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
        var offset = pageNumber == 0 ? 0 : pageNumber + 9;
        var abonents = await dbConnection.QueryAsync<AbonentDto>(
            "select Abonents.fullName, Streets.name as street, Addresses.houseNumber from Abonents " +
            "join Addresses on Abonents.id = Addresses.abonentId " +
            "join Streets on Streets.id = Addresses.streetId " +
            $"limit {count} offset {offset}");
        foreach (var abonent in abonents)
        {
            var abonentId = await dbConnection.QueryFirstOrDefaultAsync<int>($"select id from abonents where fullname='{abonent.FullName}'");
            var phoneNumbers = await dbConnection.QueryAsync<(string number, int typeId)>(
                "select phoneNumber, typeId from PhoneNumbers " +
                "join Abonents on Abonents.id = PhoneNumbers.abonentId " +
                $"where abonentId={abonentId} " +
                "order by typeId");
            foreach (var number in phoneNumbers)
            {
                if (number.typeId == 1)
                    abonent.HousePhoneNumber = number.number;
                else if (number.typeId == 2)
                    abonent.WorkPhoneNumber = number.number;
                else if (number.typeId == 3)
                    abonent.PersonalPhoneNumber = number.number;
            }
        }
        return abonents;
    }

    public async Task<int> GetAbonentsCount()
    {
        using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
        return await dbConnection.QueryFirstAsync<int>("select count(*) from abonents");
    }

    public async Task<IEnumerable<StreetDto>> GetStreets(int pageNumber, int count = 15, string search="")
    {
        using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);

        var where = string.IsNullOrWhiteSpace(search) == true ? "" : $"where name like '%{search}%'";
        
        return await dbConnection.QueryAsync<StreetDto>("select name as street, count(abonentId) as countAbonents from addresses " +
                                                 "join streets on streets.id = addresses.streetId " +
                                                 $"{where} " +
                                                 "group by street");
    }
    
    public async Task<int> GetStreetsCount()
    {
        using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
        return await dbConnection.QueryFirstAsync<int>("select count(distinct streetId) from addresses");
    }
}