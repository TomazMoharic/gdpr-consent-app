
using Microsoft.EntityFrameworkCore;

namespace GdprConsentApp.Database;

public class DatabaseInitializer
{
    // private readonly MysqlDbContext _db;
    private IServiceScopeFactory _factory;

    public DatabaseInitializer(IServiceScopeFactory factory)
    {
        _factory = factory;
    }

    public void InitializeDb()
    {
        using var scope = _factory.CreateScope();

        var db = scope.ServiceProvider.GetService<MysqlDbContext>();
        
       _ = db.Database.SqlQueryRaw<int>("CREATE DATABASE IF NOT EXISTS `db`");
       _ = db.Database.SqlQueryRaw<int>("USE `db`");
       
       _ = db.Database.SqlQueryRaw<int>(@"CREATE TABLE IF NOT EXISTS `consents` (
               `id_consent` int NOT NULL AUTO_INCREMENT,
               `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
               `code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
               `consent_duration_days` int NOT NULL DEFAULT '0',
               `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
               `updated_at` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
               PRIMARY KEY (`id_consent`)
               ) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci");
       
       _ = db.Database.SqlQueryRaw<int>(@"CREATE TABLE IF NOT EXISTS `users` (
              `id_user` int NOT NULL AUTO_INCREMENT,
              `first_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
              `last_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
              `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
              `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
              `updated_at` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
              PRIMARY KEY (`id_user`)
            ) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci");

       _ = db.Database.SqlQueryRaw<int>(@"CREATE TABLE IF NOT EXISTS `users_consents` (
            `id_user_consent` int NOT NULL AUTO_INCREMENT,
            `id_user` int NOT NULL,
            `id_consent` int NOT NULL,
            PRIMARY KEY (`id_user_consent`),
            KEY `users_consents_ibfk_2` (`id_consent`),
            KEY `users_consents_ibfk_1` (`id_user`),
            CONSTRAINT `users_consents_ibfk_1` FOREIGN KEY (`id_user`) REFERENCES `users` (`id_user`) ON DELETE CASCADE ON UPDATE CASCADE,
            CONSTRAINT `users_consents_ibfk_2` FOREIGN KEY (`id_consent`) REFERENCES `consents` (`id_consent`) ON DELETE CASCADE ON UPDATE CASCADE
          ) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci");
    }
}
