namespace Library.DataAccess
{
    public class PreOrder
    {
        public int Id { get; set; }

        public int TableId { get; set; }

        public DateTime OrderDate { get; set; }

        public string Customer { get; set; }

        public int? AccountId { get; set; }

        public virtual Account Account { get; set; } = null!;

        public virtual Table Table { get; set; } = null!;
    }
}
