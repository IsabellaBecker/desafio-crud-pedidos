import { Component } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { Pedido } from '../../models/pedido.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PedidoService } from '../../services/pedido.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-list',
  standalone: true,
  imports: [MatCardModule, MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './list.component.html',
  styleUrl: './list.component.css'
})
export class ListComponent {
  pedidos: Pedido[] = [];
  displayedColumns: string[] = ['id', 'nomeCliente', 'emailCliente', 'pago', 'actions'];

  constructor(private pedidoService: PedidoService, private snackBar: MatSnackBar, private router: Router) { }

  ngOnInit(): void {
    this.pedidoService.getAllPedidos().subscribe(data => {
      this.pedidos = data;
      console.log(this.pedidos);
    });
  }

  btnClickDetails(pedidoId: number): void {
    this.router.navigate(['/pedido', pedidoId]);
  }
}
