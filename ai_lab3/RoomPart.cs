namespace ai_lab3
{
    class RoomPart
    {
        // Збереження значення кожної частини кімнати
        private string RoomPartValue;

        public RoomPart(string value)
        {
            // Заповнення значенням кожної частини кімнати 
            // (назва предмету мебелі або _____ - пуста частина)
            RoomPartValue = value;
        }

        public string Value
        {
            get => RoomPartValue;
            set => RoomPartValue = value;
        }
    }
}
