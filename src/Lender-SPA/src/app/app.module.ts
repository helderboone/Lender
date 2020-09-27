import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component'; 

import { NavComponent } from './views/shared/nav/nav.component';
import { FooterComponent } from './views/shared/footer/footer.component';

import { FriendService } from './services/friend.service';
import { GameService } from './services/game.service';
import { LoanService } from './services/loan.service';
import { AuthService } from './services/auth.service';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { myRoutes } from './app.route';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; 
import { AuthGuardService } from './guards/auth.guard'; 
import { AlertifyService } from './services/alertify.service';
import { LoginComponent } from './views/auth/login/login.component';
import { LogoutComponent } from './views/auth/logout/logout.component';
import { AddFriendComponent } from './views/friends/add-friend/add-friend.component';
import { AddGameComponent } from './views/games/add-game/add-game.component';
import { ListFriendComponent } from './views/friends/list-friend/list-friend.component';
import { ListGameComponent } from './views/games/list-game/list-game.component';
import { ListLoanComponent } from './views/loans/list-loan/list-loan.component';
import { EditGameComponent } from './views/games/edit-game/edit-game.component';
import { EditFriendComponent } from './views/friends/edit-friend/edit-friend.component';
import { AddLoanComponent } from './views/loans/add-loan/add-loan.component';
import { DetailFriendComponent } from './views/friends/detail-friend/detail-friend.component';
import { DetailGameComponent } from './views/games/detail-game/detail-game.component';
import { DetailLoanComponent } from './views/loans/detail-loan/detail-loan.component';
import { ImageHelper } from './helpers/image.helper'; 
import { NgxSpinnerModule } from "ngx-spinner";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    LogoutComponent,
    AddFriendComponent,
    AddGameComponent,
    ListFriendComponent,
    ListGameComponent,
    ListLoanComponent,
    EditGameComponent,
    EditFriendComponent,
    AddLoanComponent,
    NavComponent, 
    FooterComponent, 
    DetailFriendComponent,
    DetailGameComponent,
    DetailLoanComponent
  ],
  imports: [
    myRoutes,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgxSpinnerModule 
  ],
  providers: [
    FriendService,
    GameService,
    LoanService,
    AuthService,
    AuthGuardService,
    AlertifyService,
    ImageHelper,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
