import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgxSpinner } from 'ngx-spinner/lib/ngx-spinner.enum';
import { ImageHelper } from 'src/app/helpers/image.helper';
import { LoanModel } from 'src/app/models/loan.model';
import { AlertifyService } from 'src/app/services/alertify.service';
import { LoanService } from 'src/app/services/loan.service';

@Component({
  selector: 'app-list-loan',
  templateUrl: './list-loan.component.html',
  styleUrls: ['./list-loan.component.css']
})
export class ListLoanComponent implements OnInit {

  loans: LoanModel[];

  constructor(private loanService: LoanService,
    private spinner: NgxSpinnerService,
    private alertifyService: AlertifyService,
    public imageHelper: ImageHelper) {
    this.populateLoan();
  }

  ngOnInit(): void {
  }

  async endLoan(friendId: number, gameId: number) {
    this.alertifyService.confirm('Voce tem certeza que quer finalizar o emprestimo ?', () => {
      this.spinner.show();
      let loan = this.loans.filter(l => l.friendId == friendId && gameId == gameId)[0];
      this.loanService.endLoan(loan).subscribe(() => {
        this.alertifyService.success('O jogo foi entregue para voce.');
        this.populateLoan();
      }, error => {         
          this.alertifyService.error('Falhou em finalizar o emprestimo');
      });
    });
  }

  populateLoan() {
    this.loanService.getLoans().subscribe(data => {
      this.loans = data;
      this.spinner.hide();
    }, error => {
      this.spinner.hide();
    });
  }
}
