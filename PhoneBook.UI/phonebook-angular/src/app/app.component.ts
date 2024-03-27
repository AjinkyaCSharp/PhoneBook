import { Component } from '@angular/core';
import { ContactService } from './contact/service/contact.service';
import { ApiService } from './contact/service/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers:[ContactService,ApiService]

})
export class AppComponent {
  title = 'phonebook-angular';
}
