namespace ServicesImplimentation
{
    public static class OrderStatus
    {
        public static string TAKE = "TAKE"; //Мастер берёт заказ в работу

        public static string FINISHED = "FINISHED"; //Мастер завершил заказ

        public static string NOT_AGREE = "NOT_AGREE"; //Мастер не согласует


        public static string WAIT_OPERATOR = "WAIT_OPERATOR"; //Ожидание действия оператора

        public static string WAIT_MASTER = "WAIT_MASTER"; //Ожидание действия мастера

        public static string WAIT_CLIENT = "WAIT_CLIENT"; //Ожидание действия клиента


        public static string REJECT = "REJECT"; //Отмена заказа клиентом

        public static string AGREED = "AGREED"; //Заказ согласован клиентом

        public static string NOT_DONE = "NOT_DONE"; //Клиента не устраивает выполненная работа

        public static string DONE = "DONE"; //Клиента устраивает выполненная работа

        public static string ACCEPTED = "ACCEPTED"; //Клиент подтвердил данные заказа

        public static string NOT_ACCEPTED = "NOT_ACCEPTED"; //Клиент не подтвердил данные заказа
    }
}
