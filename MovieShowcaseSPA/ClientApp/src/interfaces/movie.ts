import { Genre } from './genre';

export interface Movie {
  id?: number;
  title: string;
  numberInStock: number;
  dailyRentalRate: number;
  rating: number;
  genre: Genre;
  liked: boolean;
  updatedDate?: Date;
  imdbUrl?: string;
}
