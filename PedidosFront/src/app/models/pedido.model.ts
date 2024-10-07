export interface Pedido {
    id: number;
    nomeCliente: string;
    emailCliente: string;
    pago: boolean;
    dataCriacao: Date;
    valorTotal: number;
    
    itensPedido: ItemPedido[];
  }
  
  export interface ItemPedido {
    id: number;
    idPedido: number;
    idProduto: number;
    quantidade: number;
  }
  
  export interface Produto {
    id: number;
    nomeProduto: string;
    valor: number;
  }