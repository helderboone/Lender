import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FileUploader } from 'ng2-file-upload';
import { NgxSpinnerService } from 'ngx-spinner';
import { ImageHelper } from 'src/app/helpers/image.helper';
import { GameModel } from 'src/app/models/game.model';
import { AlertifyService } from 'src/app/services/alertify.service';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-edit-game',
  templateUrl: './edit-game.component.html',
  styleUrls: ['./edit-game.component.css']
})
export class EditGameComponent implements OnInit {

  @ViewChild('fileUploadInput') fileUploadInput: ElementRef;
  @ViewChild('fakeFileUploadInput') fakeFileUploadInput: ElementRef;
  editForm: FormGroup;
  gameModel: GameModel;
  submitted = false;

  public uploader: FileUploader = new FileUploader({});

  constructor(private formBuilder: FormBuilder,
              private router: Router,
              private route: ActivatedRoute,
              private gameService: GameService,
              private alertifyService: AlertifyService,
              private spinner: NgxSpinnerService,
              public imageHelper: ImageHelper) { }

  ngOnInit(): void {
    localStorage.removeItem('image');
    this.initializeForm();
    this.populateForm();
    this.setUploaderCallbacks();
  }

  setUploaderCallbacks() {
    this.uploader.onBeforeUploadItem = (item) => {
      item.withCredentials = false;
    };
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };
    this.uploader.onCompleteItem = (item: any, response: any, status: any, headers: any) => {
      console.log('FileUpload:uploaded:', item, status, response);
    };
  }

  initializeForm() {
    this.editForm = this.formBuilder.group({
      name: ['', Validators.required],
      gender: ['', Validators.required]
    });
  }

  async populateForm() {
    this.route.params.subscribe(data => {
      this.gameService.getGameById(data['id']).subscribe(data => {
        this.gameModel = JSON.parse(JSON.stringify(data));
        this.editForm.patchValue({
          name: this.gameModel.name,
          gender: this.gameModel.gender,
          photoUrl: this.gameModel.photoUrl
        });
        if (this.gameModel.photoUrl) {
          let photoUrlArray = this.gameModel.photoUrl.split('/');
          this.imageHelper.dataURLtoFile(this.gameModel.photoUrl, photoUrlArray.pop()).then(file => {
            this.gameModel.File = file;
          });
        }
      });
    });
  }

  selectFile() {
    this.fileUploadInput.nativeElement.click();
  }

  public findInvalidControls() {
    const invalid = [];
    const controls = this.editForm.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        invalid.push(name);
      }
    }
    return invalid;
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.editForm.valid) {
      const gameForm = this.editForm.value;
      gameForm.id = this.gameModel.id;
      const formData = this.populateFormData(gameForm);
      localStorage.setItem('image', gameForm.File);
      this.spinner.show();
      this.gameService.updateGame(formData)
        .subscribe(data => {
          setTimeout(() => {
            this.spinner.hide();
            this.router.navigate(['jogos']);
            this.alertifyService.success('Your game has been successfully updated!');
          }, 1000);
        },
          error => {
            console.log(error);
          });
    }
  }

  populateFormData(gameForm: any) {
    const formData = new FormData();
    gameForm.File = this.gameModel.File;
    for (let key in gameForm) {
      formData.append(key, gameForm[key]);
    }

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
