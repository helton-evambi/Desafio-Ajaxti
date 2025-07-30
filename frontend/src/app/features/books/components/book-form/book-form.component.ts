import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
} from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import {
  FormSelectComponent,
  SelectOption,
} from '../../../../shared/components/form-select/form-select.component';
import { CommonModule } from '@angular/common';
import { FormInputComponent } from '../../../../shared/components/form-input/form-input.component';
import { FormTextareaComponent } from '../../../../shared/components/form-textarea/form-textarea.component';
import { Book } from '../../models/book.model';
import { Author } from '../../../authors/models/author.model';
import { Genre } from '../../../genres/models/genre.model';

@Component({
  selector: 'app-book-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormInputComponent,
    FormTextareaComponent,
    FormSelectComponent,
  ],
  templateUrl: './book-form.component.html',
  styleUrl: './book-form.component.scss',
})
export class BookFormComponent implements OnInit, OnChanges {
  @Input() book?: Book;
  @Input() authors: Author[] = [];
  @Input() genres: Genre[] = [];
  @Input() saving: boolean = false;

  @Output() formSubmit = new EventEmitter<Book>();
  @Output() formCancel = new EventEmitter<void>();

  bookForm: FormGroup;
  authorOptions: SelectOption[] = [];
  genreOptions: SelectOption[] = [];

  constructor(private fb: FormBuilder) {
    this.bookForm = this.createForm();
  }

  ngOnInit(): void {
    this.updateOptions();
    if (this.book) {
      this.populateForm();
    }
  }

  ngOnChanges(): void {
    this.updateOptions();
    if (this.book && this.bookForm) {
      this.populateForm();
    }
  }

  private createForm(): FormGroup {
    return this.fb.group({
      title: ['', [Validators.required, Validators.minLength(2)]],
      subtitle: [''],
      description: [''],
      authorId: ['', Validators.required],
      genreId: ['', Validators.required],
      isbn: [''],
      pageCount: ['', [Validators.min(1)]],
      publisher: [''],
      publishedDate: [''],
    });
  }

  private updateOptions(): void {
    this.authorOptions = this.authors.map((author) => ({
      value: author.id,
      label: `${author.firstName} ${author.lastName}`,
    }));

    this.genreOptions = this.genres.map((genre) => ({
      value: genre.id,
      label: genre.name,
    }));
  }

  private populateForm(): void {
    if (this.book) {
      this.bookForm.patchValue({
        title: this.book.title,
        subtitle: this.book.subtitle || '',
        description: this.book.description || '',
        authorId: this.book.authorId,
        genreId: this.book.genreId,
        isbn: this.book.isbn || '',
        pageCount: this.book.pageCount || '',
        publisher: this.book.publisher || '',
        publishedDate: this.book.publishedDate || '',
      });
    }
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.bookForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  onSubmit(): void {
    if (this.bookForm.valid && !this.saving) {
      const formValue = this.bookForm.value;

      const bookData: Book = {
        ...formValue,
        id: this.book?.id,
        pageCount: formValue.pageCount
          ? parseInt(formValue.pageCount)
          : undefined,
      };

      Object.keys(bookData).forEach((key) => {
        if (
          bookData[key as keyof Book] === '' ||
          bookData[key as keyof Book] === null
        ) {
          delete bookData[key as keyof Book];
        }
      });

      this.formSubmit.emit(bookData);
    } else {
      this.markFormGroupTouched();
    }
  }

  onCancel(): void {
    this.bookForm.reset();
    this.formCancel.emit();
  }

  private markFormGroupTouched(): void {
    Object.keys(this.bookForm.controls).forEach((key) => {
      const control = this.bookForm.get(key);
      control?.markAsTouched();
    });
  }

  resetForm(): void {
    this.bookForm.reset();
  }
}
