import { Routes } from '@angular/router';
import { ListComponent } from './components/list/list.component';
import { FormComponent } from './components/form/form.component';

export const routes: Routes = [
    { path: 'products', component: ListComponent },
    { path: 'product/:id', component: FormComponent },
    { path: '', redirectTo: '/products', pathMatch: 'full' },
  ];
