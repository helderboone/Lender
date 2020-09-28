import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { FriendModel } from 'src/app/models/friend.model';
import { GameModel } from 'src/app/models/game.model';
import { LoanModel } from 'src/app/models/loan.model';
import { AlertifyService } from 'src/app/services/alertify.service';
import { FriendService } from 'src/app/services/friend.service';
import { GameService } from 'src/app/services/game.service';
import { LoanService } from 'src/app/services/loan.service';

@Component({
  selector: 'app-add-loan',
  templateUrl: './add-loan.component.html',
  styleUrls: ['./add-loan.component.css']
})
export class AddLoanComponent implements OnInit {

  addForm: FormGroup;
  loanModel: LoanModel = {
    friendId: 0,
    gameId: 0 
  };

  games: GameModel[];
  friends: FriendModel[];

  submitted = false;

  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private gameService: GameService,
    private friendService: FriendService,
    private loanService: LoanService,
    private alertifyService: AlertifyService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.initializeForm();
    this.populateForm();
  }

  populateForm() {
    this.gameService.getNotBorrowedGames().subscribe(data => {
      this.games = data;
    });

    this.friendService.getFriends().subscribe(data => {
      this.friends = data;
    });
  }

  initializeForm() {
    this.addForm = this.formBuilder.group({      
      gameId: ['', Validators.required],
      friendId: ['', Validators.required]
    });
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.addForm.valid) {  
      this.loanModel = this.addForm.value;
      console.log(this.addForm.value);
      Object.assign(this.loanModel, { friendId: parseInt(this.addForm.value.friendId), gameId: parseInt(this.addForm.value.gameId)});

      this.spinner.show();
      this.loanService.addLoan(this.loanModel)
        .subscribe(data => {
          setTimeout(() => {
            this.spinner.hide();
            this.router.navigate(['emprestimos']);
            this.alertifyService.success("O jogo foi emprestado!");
          }, 1000);
        },
          error => {
            console.log(error)
          });
    }
  }
}
