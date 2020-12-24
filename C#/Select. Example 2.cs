namespace Terrasoft.Configuration.VSSales
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading;
    using Terrasoft.Common;
    using Terrasoft.Core;
    using Terrasoft.Core.DB;
    using Terrasoft.Core.Entities;
    using Terrasoft.Core.Entities.Events;

    [EntityEventListener(SchemaName = "ScPriceType")]
    public class ScPriceTypeEventListener : BaseEntityEventListener
    {
        public override void OnSaving(object sender, EntityBeforeEventArgs e) {
            base.OnSaving(sender, e);
            var entity = (Entity)sender;
            if (!(entity is null)) {
                var userConnection = entity?.UserConnection;
                if (!CheckRulesDbExecutor(userConnection, entity)) {
                    e.IsCanceled = true;
                }
            }
        }

        private List<ScPriceTypeEntity> GetRecords(UserConnection userConnection, ScPriceTypeEntity us) {
            var records = new List<ScPriceTypeEntity>();
            // SQL-запрос.
            var select = (Select)new Select(userConnection)
                .Column("Id")
                .Column("AccountId")            // контрагент
                .Column("StartDate")            // дата начала
                .Column("EndDate")              // дата окончания
                .Column("vsDeliveryLocationId") // место досатвки
                .Column("vsLogisticTypeId")     // тип логистики
                .From("ScPriceType")            // таблица "Коммерческие условия"
                .Where()
                .OpenBlock("AccountId").IsEqual(Column.Parameter(us.AccountId)) // контрагент
                .And("Id").IsNotEqual(Column.Parameter(us.Id))     // исключить саму запись
                .CloseBlock();
            // место доставки
            var deliveryCondition = new QueryCondition() {
                ConditionType = us.DeliveryLocationId == Guid.Empty ? QueryConditionType.IsNull : QueryConditionType.Equal,
                LeftExpression = new QueryColumnExpression(Column.SourceColumn("vsDeliveryLocationId")),
                RightExpressions = { Column.Parameter(us.DeliveryLocationId) }
            };
            select.AddCondition(deliveryCondition, LogicalOperation.And);
            // тип логистики
            var logisticTypeCondition = new QueryCondition() {
                ConditionType = us.LogisticTypeId == Guid.Empty ? QueryConditionType.IsNull : QueryConditionType.Equal,
                LeftExpression = new QueryColumnExpression(Column.SourceColumn("vsLogisticTypeId")),
                RightExpressions = { Column.Parameter(us.LogisticTypeId) }
            };
            select.AddCondition(logisticTypeCondition, LogicalOperation.And);
            // Использование экземпляра DBExecutor в основном потоке.
            using (DBExecutor dbExecutor = userConnection.EnsureDBConnection())
            {
                using (IDataReader dataReader = select.ExecuteReader(dbExecutor))
                {
                    while (dataReader.Read())
                    {
                        records.Add(new ScPriceTypeEntity(
                            dataReader.GetColumnValue<Guid>("Id"),
                            dataReader.GetColumnValue<Guid>("AccountId"),
                            dataReader.GetColumnValue<DateTime>("StartDate"),
                            dataReader.GetColumnValue<DateTime>("EndDate"),
                            dataReader.GetColumnValue<Guid>("vsDeliveryLocationId"),
                            dataReader.GetColumnValue<Guid>("vsLogisticTypeId")));
                    }
                }
            }
            return records;
        }

        private bool CheckRulesDbExecutor(UserConnection userConnection, Entity entity) {
            // Значения полей.
            var us = new ScPriceTypeEntity(entity);

            // Список записей таблицы "Коммерческие условия".
            var records = GetRecords(userConnection, us);

            // Найдены записи.
            if (records.Count > 0) {
                var caption = "Контроль коммерческих условий." + Environment.NewLine;
                // 1. Совпадение дат.
                string msg;
                if (records.Any(rec => rec.StartDate == us.StartDate && rec.EndDate == us.EndDate)) {
                    msg = "Совпадение дат (код 1)!";
                    throw new Exception(caption + msg);
                }
                // 2. Нет дат завершения.
                if (records.Any(rec => us.EndDate.IsNullOrEmpty() && rec.EndDate.IsNullOrEmpty()))
                {
                    msg = "Нет дат завершения (код 2)!";
                    throw new Exception(caption + msg);
                }
                // 3. Дата начала пользователя меньше даты завершения записей и нет даты завершения пользователя.
                if (records.Any(rec => us.StartDate <= rec.EndDate && us.EndDate.IsNullOrEmpty()))
                {
                    msg = "Пересечение дат (код 3)!";
                    throw new Exception(caption + msg);
                }
                // 4. Дата завершения пользователя меньше даты начала записей и нет даты завершения записей.
                if (records.Any(rec => us.EndDate >= rec.StartDate && rec.EndDate.IsNullOrEmpty()))
                {
                    msg = "Пересечение дат (код 4)!";
                    throw new Exception(caption + msg);
                }
                // 5.1. Вхождение дат.
                if (records.Any(rec =>
                    (!rec.StartDate.IsNullOrEmpty() && !rec.EndDate.IsNullOrEmpty() && !us.StartDate.IsNullOrEmpty() && !us.EndDate.IsNullOrEmpty()) &&
                    us.StartDate >= rec.StartDate && us.StartDate <= rec.EndDate && us.EndDate >= rec.StartDate && us.EndDate <= rec.EndDate))
                {
                    msg = "Пересечение дат (код 5.1)!";
                    throw new Exception(caption + msg);
                }
                // 5.2. Вхождение дат.
                if (records.Any(rec =>
                    (!rec.StartDate.IsNullOrEmpty() && !rec.EndDate.IsNullOrEmpty() && !us.StartDate.IsNullOrEmpty() && !us.EndDate.IsNullOrEmpty()) &&
                    us.StartDate <= rec.StartDate && us.EndDate >= rec.StartDate && us.EndDate <= rec.EndDate))
                {
                    msg = "Пересечение дат (код 5.2)!";
                    throw new Exception(caption + msg);
                }
                // 5.3. Вхождение дат.
                if (records.Any(rec =>
                    (!rec.StartDate.IsNullOrEmpty() && !rec.EndDate.IsNullOrEmpty() && !us.StartDate.IsNullOrEmpty() && !us.EndDate.IsNullOrEmpty()) &&
                    us.StartDate >= rec.StartDate && us.StartDate <= rec.EndDate && us.EndDate >= rec.EndDate))
                {
                    msg = "Пересечение дат (код 5.3)!";
                    throw new Exception(caption + msg);
                }
                // 5.4. Вхождение дат.
                if (records.Any(rec =>
                    (!rec.StartDate.IsNullOrEmpty() && !rec.EndDate.IsNullOrEmpty() && !us.StartDate.IsNullOrEmpty() && !us.EndDate.IsNullOrEmpty()) &&
                    us.StartDate >= rec.StartDate && us.StartDate <= rec.EndDate && us.EndDate >= rec.StartDate && us.EndDate <= rec.EndDate))
                {
                    msg = "Пересечение дат (код 5.4)!";
                    throw new Exception(caption + msg);
                }
            }
            return true;
        }
    }

    public class ScPriceTypeEntity
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public DateTime? StartDate { get; }
        public DateTime? EndDate { get; }
        public Guid DeliveryLocationId { get; set; }
        public Guid LogisticTypeId { get; set; }

        public ScPriceTypeEntity(Entity entity) {
            // Id.
            var objId = entity.GetColumnValue("Id");
            Id = (Guid?)objId ?? Guid.Empty;
            // Контрагент.
            var objAccountId = entity.GetColumnValue("AccountId");
            AccountId = (Guid?)objAccountId ?? Guid.Empty;
            // Дата начала.
            var objStartDate = entity.GetColumnValue("StartDate");
            StartDate = (DateTime?)objStartDate;
            StartDate = StartDate?.Date;
            // Дата окончания.
            var objEndDate = entity.GetColumnValue("EndDate");
            EndDate = (DateTime?)objEndDate;
            EndDate = EndDate?.Date;
            // Место доставки.
            var objDeliveryLocation = entity.GetColumnValue("vsDeliveryLocationId");
            DeliveryLocationId = (Guid?)objDeliveryLocation ?? Guid.Empty;
            // Тип логистики.
            var objLogisticType = entity.GetColumnValue("vsLogisticTypeId");
            LogisticTypeId = (Guid?)objLogisticType ?? Guid.Empty;
        }

        public ScPriceTypeEntity(Guid id, Guid accountId, DateTime? startDate, DateTime? endDate, Guid deliveryLocationId, Guid logisticTypeId)
        {
            // Id.
            Id = id;
            // Контрагент.
            AccountId = accountId;
            // Дата начала.
            StartDate = startDate;
            // Дата окончания.
            EndDate = endDate;
            // Место доставки.
            DeliveryLocationId = deliveryLocationId;
            // Тип логистики.
            LogisticTypeId = logisticTypeId;
        }

        public override string ToString() {
            var strStartDate = StartDate is null ? "null" : StartDate.ToString();
            var strEndDate = EndDate is null ? "null" : EndDate.ToString();
            return $"Id: {Id}. AccountId: {AccountId}. StartDate: {strStartDate}. EndDate: {strEndDate}. DeliveryLocationId: {DeliveryLocationId}. LogisticTypeId: {LogisticTypeId}.";
        }
    }

    public static class DtExtnesion
    {
        public static bool IsNullOrEmpty(this DateTime? dt) {
            return dt is null ||
                dt.Value.Date == new DateTime(0001, 01, 01).Date;
        }
    }
}
