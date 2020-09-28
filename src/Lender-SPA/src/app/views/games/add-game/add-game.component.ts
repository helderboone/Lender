import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FileUploader } from 'ng2-file-upload';
import { NgxSpinnerService } from 'ngx-spinner';
import { ImageHelper } from 'src/app/helpers/image.helper';
import { GameModel } from 'src/app/models/game.model';
import { AlertifyService } from 'src/app/services/alertify.service';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-add-game',
  templateUrl: './add-game.component.html',
  styleUrls: ['./add-game.component.css']
})
export class AddGameComponent implements OnInit {

  @ViewChild('fileUploadInput') fileUploadInput: ElementRef;
  @ViewChild('fakeFileUploadInput') fakeFileUploadInput: ElementRef;
  
  addForm: FormGroup;
  gameModel: GameModel = {
    id: 0,
    name: '',
    gender: '',
    photoPublicId: '',
    photoUrl: '',
    File: null
  };
  
  submitted = false;

  public uploader: FileUploader = new FileUploader({});

  constructor(private formBuilder: FormBuilder, 
    private router : Router,
    private gameService: GameService,
    private alertifyService: AlertifyService,
    private spinner: NgxSpinnerService,
    public imageHelper: ImageHelper) { }

  ngOnInit(): void {
    localStorage.removeItem('image');
    this.initializeForm(); 
    this.setUploaderCallbacks();
  }

  setUploaderCallbacks() {
    this.uploader.onBeforeUploadItem = (item) => {
      item.withCredentials = false;
    }
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };
    this.uploader.onCompleteItem = (item: any, response: any, status: any, headers: any) => {
      console.log('FileUpload:uploaded:', item, status, response);
    };
  }

  initializeForm() {
    this.addForm = this.formBuilder.group({
      name: ['', Validators.required],
      gender: ['', Validators.required]
    });
  } 

  selectFile() {
    this.fileUploadInput.nativeElement.click();
  } 

  onSubmit(): void {
    this.submitted = true;
    if (this.addForm.valid) {
      let gameForm = this.addForm.value; 
      const formData = this.populateFormData(gameForm);
      localStorage.setItem('image', gameForm.File);
      this.spinner.show();
      this.gameService.addGame(formData)
        .subscribe(data => {
          setTimeout(() => {
            this.spinner.hide();
            this.router.navigate(['jogos']);
            this.alertifyService.success("O jogo foi adicionado com sucesso!");
          }, 1000);
        },
        error => {
            console.log(error)
        });
    }
  }

  populateFormData(gameForm: any) {
    const formData = new FormData();
    gameForm.File = this.gameModel.File;
    for (var key in gameForm)
      formData.append(key, gameForm[key]);

    return formData;
  }

  handleFileSelect(evt) {
    const files = evt.target.files;
    const file = files[0];

    if (files && file) {
      this.gameModel.File = file;
      const reader = new FileReader();
      reader.onload = this._handleReaderLoaded.bind(this);
      reader.readAsBinaryString(file);
    }
  }

  _handleReaderLoaded(readerEvt) {
    const binaryString = readerEvt.target.result;
    this.gameModel.photoUrl = 'data:image/jpg;base64,' + btoa(binaryString);
  }

}
