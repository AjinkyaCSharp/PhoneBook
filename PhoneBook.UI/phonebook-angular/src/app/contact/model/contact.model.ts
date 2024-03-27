export class Contact{
    id?:string;
    firstName:string;
    lastName:string;
    email:string;
    primaryContact:string;
    secondaryContact:string;
    address:string;

    constructor(id,firstName,lastName,email,primaryContact,secondaryContact,address){
        this.id=id;
        this.firstName=firstName;
        this.lastName=lastName;
        this.email=email;
        this.primaryContact=primaryContact;
        this.secondaryContact=secondaryContact;
        this.address=address;
    }
}