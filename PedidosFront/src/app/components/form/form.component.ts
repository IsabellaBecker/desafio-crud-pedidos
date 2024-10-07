import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { Pedido } from '../../models/pedido.model';
import { PedidoService } from '../../services/pedido.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';

@Component({
  selector: 'app-form',
  standalone: true,
  imports: [MatCardModule, FormsModule, MatInputModule, MatButtonModule, MatCheckboxModule],
  templateUrl: './form.component.html',
  styleUrl: './form.component.css'
})
export class FormComponent {
  pedido: Pedido =  {
    id: 0,
    nomeCliente: '',
    emailCliente: '',
    pago: false,
    dataCriacao: new Date('2000-01-01'),
    valorTotal: 0,
    itensPedido: []
  };

  isEditMode = false;
  isChecked = false;

  constructor(private pedidoService: PedidoService, private router: Router) { }

  ngOnInit(): void {
    var pedidoId = 1

    if (pedidoId) {
      this.isEditMode = true;
      this.pedidoService.getPedidoById(pedidoId).subscribe(data => {
        this.pedido = data;
      });
    }
  }

  onSubmit(): void {}
}
