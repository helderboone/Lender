import { RouterModule, Routes } from "@angular/router"; 
import { AuthGuardService } from './guards/auth.guard'; 
import { LoginComponent } from './views/auth/login/login.component';
import { LogoutComponent } from './views/auth/logout/logout.component';
import { AddFriendComponent } from './views/friends/add-friend/add-friend.component';
import { DetailFriendComponent } from './views/friends/detail-friend/detail-friend.component';
import { EditFriendComponent } from './views/friends/edit-friend/edit-friend.component';
import { ListFriendComponent } from './views/friends/list-friend/list-friend.component';
import { AddGameComponent } from './views/games/add-game/add-game.component';
import { DetailGameComponent } from './views/games/detail-game/detail-game.component';
import { EditGameComponent } from './views/games/edit-game/edit-game.component';
import { ListGameComponent } from './views/games/list-game/list-game.component'; 
import { AddLoanComponent } from './views/loans/add-loan/add-loan.component';
import { DetailLoanComponent } from './views/loans/detail-loan/detail-loan.component';
import { ListLoanComponent } from './views/loans/list-loan/list-loan.component'; 

const MY_ROUTES: Routes = [
    { path: '', redirectTo: 'amigos', pathMatch: 'full' },   
    { path: 'amigos', component: ListFriendComponent, canActivate: [AuthGuardService] },
    { path: 'amigos/:id', component: DetailFriendComponent, canActivate: [AuthGuardService] },
    { path: 'adicionar-amigo', component: AddFriendComponent, canActivate: [AuthGuardService] },
    { path: 'editar-amigo/:id', component: EditFriendComponent, canActivate: [AuthGuardService]},
    { path: 'jogos', component: ListGameComponent, canActivate: [AuthGuardService] },
    { path: 'jogos/:id', component: DetailGameComponent, canActivate: [AuthGuardService] },
    { path: 'adicionar-jogo', component: AddGameComponent, canActivate: [AuthGuardService] },
    { path: 'editar-jogo/:id', component: EditGameComponent, canActivate: [AuthGuardService] },    
    { path: 'emprestimos', component: ListLoanComponent, canActivate: [AuthGuardService] },    
    { path: 'adicionar-emprestimo', component: AddLoanComponent, canActivate: [AuthGuardService] },    
    { path: 'emprestimos/:id', component: DetailLoanComponent, canActivate: [AuthGuardService] },    
    { path: 'login', component: LoginComponent },
    { path: 'sair', component: LogoutComponent, canActivate: [AuthGuardService] },
    { path: '**', redirectTo: '/' }
];

export const myRoutes = RouterModule.forRoot(MY_ROUTES);
