import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ImageHelper } from 'src/app/helpers/image.helper';
import { GameModel } from 'src/app/models/game.model';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-detail-game',
  templateUrl: './detail-game.component.html',
  styleUrls: ['./detail-game.component.css']
})
export class DetailGameComponent implements OnInit {

  public gameModel: GameModel;

  constructor(private route: ActivatedRoute, private gameService: GameService, public imageHelper: ImageHelper) {
    this.route.params.subscribe(data => {
      this.gameService.getGameById(data["id"]).subscribe(data => {
        this.gameModel = JSON.parse(JSON.stringify(data));
      });
    });
  }

  ngOnInit(): void {
  }
}
