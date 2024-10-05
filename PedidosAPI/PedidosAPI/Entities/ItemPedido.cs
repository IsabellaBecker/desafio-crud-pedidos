namespace PedidosAPI.Entidades
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdProduto { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
