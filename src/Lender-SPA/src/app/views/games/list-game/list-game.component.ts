import { Component, OnInit } from '@angular/core';
import { ImageHelper } from 'src/app/helpers/image.helper';
import { GameModel } from 'src/app/models/game.model';
import { AlertifyService } from 'src/app/services/alertify.service';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-list-game',
  templateUrl: './list-game.component.html',
  styleUrls: ['./list-game.component.css']
})
export class ListGameComponent implements OnInit {

  games: GameModel[]; 

  constructor(private gameService: GameService,
    private alertifyService: AlertifyService,
    public imageHelper: ImageHelper) {
      this.populateGames();
  }

  ngOnInit(): void {
  }

  remove(id: number) {
    this.alertifyService.confirm('Voce tem certeza que quer deletar esse jogo?', () => {
      this.gameService.removeGame(id).subscribe(() => {
        this.alertifyService.success('O jogo foi deletado');
        this.populateGames();
      }, error => { 
        if (error.error && error.error[0].Message)
          this.alertifyService.error(error.error[0].Message);
        else
          this.alertifyService.error('Falhou em deletar o jogo');
      });
    });
  }

  populateGames(){
    this.gameService.getGames().subscribe(data => {
      this.games = data;
    });
  }

}
