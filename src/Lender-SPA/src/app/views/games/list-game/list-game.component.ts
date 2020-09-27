import { Component, OnInit } from '@angular/core';
import { GameModel } from 'src/app/models/game.model';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-list-game',
  templateUrl: './list-game.component.html',
  styleUrls: ['./list-game.component.css']
})
export class ListGameComponent implements OnInit {

  games: GameModel[];

  constructor(private gameService: GameService) {
    this.gameService.getGames().subscribe(data => {
      this.games = data;
    });
  }

  ngOnInit(): void {
  }
}
