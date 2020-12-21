// ------------------------------------------------------------------------------------------------------------------------
// Select
// https://academy.terrasoft.ru/documents/technic-sdk/7-11/ispolzovanie-entityschemaquery-dlya-postroeniya-zaprosov-k-baze-dannyh
// ------------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Terrasoft.Common;
using Terrasoft.Core;
using Terrasoft.Core.DB;
using Terrasoft.Core.Entities;

// Создание экземпляра запроса, добавление в запрос колонок и источника данных.
Select selectQuery = new Select(UserConnection).Column("Id").Column("Name").From("Contact");
// Выполнение запроса к базе данных и получение результирующего набора данных.
using (DBExecutor dbExecutor = UserConnection.EnsureDBConnection())
{
    using (IDataReader reader = selectQuery.ExecuteReader(dbExecutor))
    {
        while (reader.Read())
        {
            // Обработка результатов запроса.
        }
    }
}

// Создание экземпляра запроса EntitySchemaQuery.
EntitySchemaQuery esq = new EntitySchemaQuery(UserConnection.EntitySchemaManager, "SomeSchema");
esq.AddColumn("SomeColumn");
// Получение экземпляра Select, ассоциированного с созданным запросом EntitySchemaQuery.
Select selectEsq = esq.GetSelectQuery(UserConnection); 
