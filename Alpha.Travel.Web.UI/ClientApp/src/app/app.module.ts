import { NgModule } from '@angular/core';

// Components
import { AppComponent } from './app.component';
import { DestinationsComponent } from './destinations/destinations.component';
import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';

// Services
import { DestinationService } from './destination.service';

// Modules
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRouterModule } from './app-router.module';

@NgModule({
  declarations: [
    AppComponent,
    DestinationsComponent,
    FooterComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    AppRouterModule,
    HttpClientModule
  ],
  providers: [DestinationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
