import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { FormInputComponent } from '../../../../shared/components/form-input/form-input.component';
import { FormTextareaComponent } from '../../../../shared/components/form-textarea/form-textarea.component';
import { Genre } from '../../models/genre.model';

@Component({
  selector: 'app-genre-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormInputComponent,
    FormTextareaComponent,
  ],
  templateUrl: './genre-form.component.html',
  styleUrl: './genre-form.component.scss',
})
export class GenreFormComponent implements OnInit, OnChanges {
  @Input() genre?: Genre;
  @Input() saving: boolean = false;

  @Output() formSubmit = new EventEmitter<Genre>();
  @Output() formCancel = new EventEmitter<void>();

  genreForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.genreForm = this.createForm();
  }

  ngOnInit(): void {
    if (this.genre) {
      this.populateForm();
    }
  }

  ngOnChanges(): void {
    if (this.genre && this.genreForm) {
      this.populateForm();
    }
  }

  private createForm(): FormGroup {
    return this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      description: [''],
    });
  }

  private populateForm(): void {
    if (this.genre) {
      this.genreForm.patchValue({
        name: this.genre.name,
        description: this.genre.description || '',
      });
    }
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.genreForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  onSubmit(): void {
    if (this.genreForm.valid && !this.saving) {
      const formValue = this.genreForm.value;

      const genreData: Genre = {
        ...formValue,
        id: this.genre?.id,
      };

      Object.keys(genreData).forEach((key) => {
        if (
          genreData[key as keyof Genre] === '' ||
          genreData[key as keyof Genre] === null
        ) {
          delete genreData[key as keyof Genre];
        }
      });

      this.formSubmit.emit(genreData);
    } else {
      this.markFormGroupTouched();
    }
  }

  onCancel(): void {
    this.genreForm.reset();
    this.formCancel.emit();
  }

  private markFormGroupTouched(): void {
    Object.keys(this.genreForm.controls).forEach((key) => {
      const control = this.genreForm.get(key);
      control?.markAsTouched();
    });
  }

  resetForm(): void {
    this.genreForm.reset();
  }
}
