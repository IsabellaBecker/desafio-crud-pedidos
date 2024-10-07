namespace PedidosAPI.Entidades
{
    public class Pedido
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Pago { get; set; }
        public ICollection<ItemPedido> ItensPedido { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
