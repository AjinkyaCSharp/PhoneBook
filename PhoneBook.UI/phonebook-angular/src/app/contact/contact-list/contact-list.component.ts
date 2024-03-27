import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { Contact } from '../model/contact.model';
import { ContactService } from '../service/contact.service';
import { Subscription } from 'rxjs';
import { ApiService } from '../service/api.service';
const nisPackage = require("../../../../package.json");
@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css'],
})
export class ContactListComponent implements OnInit,OnDestroy {

  nisVersion = nisPackage.dependencies["ngx-infinite-scroll"];
  isLoading=false;
  contacts:Contact[]=[];
  contactsUpdatedSubscription:Subscription;
  pageNo=0;
  itemsPerPage=5;
  constructor(private contactService:ContactService,private apiService:ApiService){
  
  }
  ngOnDestroy(): void {
    this.contactsUpdatedSubscription.unsubscribe();
  }
  @HostListener('window:scroll',['$event'])
  onWindowScroll(event:any){
    
    if(window.innerHeight+window.scrollY>=document.body.offsetHeight&&!this.isLoading){
      console.log(event);
      this.onScrollDown()
    }
  }
  ngOnInit(): void {
    this.contacts=this.contactService.getContacts();
    this.contactsUpdatedSubscription=this.contactService.contactsUpdatedEvent.subscribe(()=>{
      this.contacts=this.contactService.getContacts();
    })
    this.apiService.getContact(this.pageNo,this.itemsPerPage).subscribe((contacts:Contact[])=>{
        this.contactService.setContacts(contacts);
        this.isLoading=false;
        this.pageNo++;
    });
  }

  onScrollDown(){
      this.apiService.getContact(this.pageNo*this.itemsPerPage,this.itemsPerPage).subscribe((contacts:Contact[])=>{
        this.contactService.setContacts(contacts);
        this.isLoading=false;
        this.pageNo++;
    });
  }
  
  onScrollUp(){
    this.apiService.getContact(this.pageNo*this.itemsPerPage,this.itemsPerPage).subscribe((contacts:Contact[])=>{
      this.contactService.setContacts(contacts);
      this.isLoading=false;
      this.pageNo++;
  });
  }
}

