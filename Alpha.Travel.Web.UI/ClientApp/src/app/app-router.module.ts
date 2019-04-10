import { RouterModule, Routes } from '@angular/router';
// Components
import { DestinationsComponent } from './destinations/destinations.component';
import { NgModule } from '@angular/core';

// route

const routes: Routes = [
  { path: '', component: DestinationsComponent },
  { path: 'destinations', component: DestinationsComponent }
]


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRouterModule { }
