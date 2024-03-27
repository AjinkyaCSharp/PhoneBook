import {Injectable} from "@angular/core"
import { Contact } from "../model/contact.model";
import { Subject, lastValueFrom } from "rxjs";
import { ApiService } from "./api.service";
@Injectable({
    providedIn: 'root'
  })
export class ContactService{
    contactsUpdatedEvent=new Subject<void>();
    private contactList:Contact[]=[];
    constructor(private apiService:ApiService){
       
    }
    getContacts():Contact[]{
        return this.contactList.slice();
    }
    setContacts(contacts:Contact[]){
        this.contactList=contacts;
        this.contactsUpdatedEvent.next();
    }
    getContactById(id:string){
        return this.contactList.slice().find((x)=>x.id===id);
    }    

    addContact(conatct:Contact){
        this.contactList.push(conatct);
        this.contactsUpdatedEvent.next();
    }

    deleteContact(id){
        let index=this.contactList.findIndex((x)=>x.id==id);
        this.contactList.splice(index,1);
        this.contactsUpdatedEvent.next();
    }
}