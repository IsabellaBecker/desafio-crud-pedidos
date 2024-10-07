import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Pedido } from '../models/pedido.model';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {
  private apiUrl = 'https://localhost:7015/api/Pedido';

  constructor(private HttpClient: HttpClient) { }

  getAllPedidos(): Observable<Pedido[]> {
    return this.HttpClient.get<Pedido[]>(this.apiUrl);
  }

  getPedidoById(id: number): Observable<Pedido> {
    return this.HttpClient.get<Pedido>(`${this.apiUrl}/${id}`);
  }

  createPedido(pedido: Pedido): Observable<Pedido> {
    return this.HttpClient.post<Pedido>(this.apiUrl, pedido);
  }

  updatePedido(id: number, pedido: Pedido): Observable<void> {
    return this.HttpClient.put<void>(`${this.apiUrl}/${id}`, pedido);
  }

  deletePedido(id: number): Observable<void> {
    return this.HttpClient.delete<void>(`${this.apiUrl}/${id}`);
  }
}
