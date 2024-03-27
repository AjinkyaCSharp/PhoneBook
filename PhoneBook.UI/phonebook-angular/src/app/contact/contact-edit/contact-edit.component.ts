import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ContactService } from '../service/contact.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../service/api.service';
import { Contact } from '../model/contact.model';
@Component({
  selector: 'app-contact-edit',
  templateUrl: './contact-edit.component.html',
  styleUrls: ['./contact-edit.component.css']
})
export class ContactEditComponent implements OnInit{
  
  contactForm:FormGroup;
  isLoading:false;
  constructor(private contactService:ContactService
    ,private router:Router
    ,private activatedRoute:ActivatedRoute
    ,private apiService:ApiService){}
  
  ngOnInit(): void {
    this.contactForm=new FormGroup(
      {'firstName':new FormControl(null,[Validators.required,Validators.maxLength(100)]),
      'lastName':new FormControl(null,[Validators.required,Validators.maxLength(100)]),
      'email':new FormControl(null,[Validators.required,Validators.email]),
      'primaryContact':new FormControl(null,[Validators.required,Validators.pattern('^[0-9]{6,10}$')]),
      'secondaryContact':new FormControl(null,[Validators.pattern('^[0-9]{6,10}$')]),
      'address':new FormControl(null)
     }
    )
  }

  onSubmit(){

    let firstName= this.contactForm.get('firstName',).value;
    let lastName=this.contactForm.get('lastName').value;
    let email=this.contactForm.get('email').value;
    let primaryContact=this.contactForm.get('primaryContact').value;
    let secondaryContact=this.contactForm.get('secondaryContact').value;
    let address=this.contactForm.get('address').value;
    let contact=new Contact(null,firstName,lastName,email,primaryContact,secondaryContact,address);
    this.apiService.addContact(contact).subscribe((id:string)=>{
        contact.id=id;
        //this.contactService.addContact(contact);    
        this.router.navigate(['/contact'],{relativeTo:this.activatedRoute});
    });
  }
  onClear(){
    this.contactForm.reset();
  }
}
