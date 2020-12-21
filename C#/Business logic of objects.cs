// ------------------------------------------------------------------------------------------------------------------------
// Business logic of objects / Бизнес-логика объектов
// https://academy.terrasoft.ru/docs/developer/back-end_development/entity_event_layer/sobytiynyy_sloy_obekta
// https://academy.terrasoft.ru/docs/7-17/developer/back-end_development/entity_event_layer/sobytiynyy_sloy_obekta
// https://academy.terrasoft.ru/documents/technic-sdk/7-16/sobytiynyy-sloy-obekta
/*
1. Создать класс, наследующий BaseEntityEventListener.
2. Декорировать класс атрибутом EntityEventListener с указанием имени сущности, для которой необходимо выполнить подписку событий.
3. Переопределить метод-обработчик нужного события.
Важно. В классе, реализующем интерфейс IEntityEventAsyncOperation, нежелательно описывать логику изменения основной сущности. 
Подобное использование может привести к неверному формированию данных. Также не стоит выполнять легковесные операции (например, подсчет значения поля), 
т. к. создание отдельного потока может занимать больше времени, чем выполнение самой операции.
*/
// ------------------------------------------------------------------------------------------------------------------------
namespace Terrasoft.Configuration.VSSales
{
    using Terrasoft.Common;
    using Terrasoft.Core;
    using Terrasoft.Core.Entities;
    using Terrasoft.Core.Entities.AsyncOperations;
    using Terrasoft.Core.Entities.AsyncOperations.Interfaces;
    using Terrasoft.Core.Entities.Events;
    using Terrasoft.Core.Factories;

    // Слушатель событий сущности "Коммерческие условия".
    [EntityEventListener(SchemaName = "ScPriceType")]
    public class ScPriceTypeEventListener : BaseEntityEventListener
    {
        // Обработчик события перед сохранением записи.
        public override void OnSaving(object sender, EntityBeforeEventArgs e)
        {
            base.OnSaving(sender, e);
            
            // Экземпляр класса для асинхронного выполнения.
            var asyncExecutor = ClassFactory.Get<IEntityEventAsyncExecutor>();
            // Параметры для асинхронного выполнения.
            var operationArgs = new EntityEventAsyncOperationArgs((Entity)sender, e);
            // Выполнение в асинхронном режиме.
            asyncExecutor.ExecuteAsync<OnSavingAsync>(operationArgs);
        }
    }

    /// <summary>
    /// Перед сохранением записи.
    /// </summary>
    public class OnSavingAsync : IEntityEventAsyncOperation
    {
        // Стартовый метод класса.
        public void Execute(UserConnection userConnection, EntityEventAsyncOperationArgs arguments)
        {
            //
        }
    }
}
