import { Component, Input, OnInit } from '@angular/core';
import { Contact } from '../model/contact.model';
import { ContactService } from '../service/contact.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../service/api.service';
@Component({
  selector: 'app-contact-detail',
  templateUrl: './contact-detail.component.html',
  styleUrls: ['./contact-detail.component.css']
})
export class ContactDetailComponent implements OnInit {
    contact:Contact;
    id:string;
    constructor(private contactService:ContactService,private activatedRoute:ActivatedRoute,private router:Router,
      private apiservice:ApiService){}

    ngOnInit(): void {
      this.activatedRoute.params.subscribe((params)=>{
      this.contact=this.contactService.getContactById(params['id'])
      this.id=params['id'];
      })
    }

    deleteContact(){
      this.apiservice.deleteContact(this.id).subscribe(()=>{
      this.contactService.deleteContact(this.id);
      this.router.navigate(['/contact'],{relativeTo:this.activatedRoute})
      });
    }
}
