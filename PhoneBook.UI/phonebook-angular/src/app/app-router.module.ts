import {NgModule} from '@angular/core'
import {RouterModule} from '@angular/router'
import { ContactEditComponent } from './contact/contact-edit/contact-edit.component';
import { ContactComponent } from './contact/contact.component';
import { ContactDetailComponent } from './contact/contact-detail/contact-detail.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { VerifyEmailComponent } from './verify-email/verify-email.component';
import { AuthGuard } from './contact/guard/authguard.guard';
type PathMatch = "full" | "prefix" | undefined;
const appRoutes=[
    { path: '', redirectTo: '/sign-in', pathMatch: 'full' as PathMatch },
  { path: 'sign-in', component: SignInComponent },
  { path: 'register-user', component: SignUpComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'verify-email-address', component: VerifyEmailComponent },
    {path:"contact",component:ContactComponent,children:[
        {path:":id",component:ContactDetailComponent}
    ], canActivate:[AuthGuard]},
    {path:"edit-contact",component:ContactEditComponent,canActivate:[AuthGuard]}
];
@NgModule({
    imports:[RouterModule.forRoot(appRoutes)],
    exports:[RouterModule]
})
export class AppRouterModule{

}