import { Genre } from './genre';

export interface Movie {
  id: number;
  title: string;
  numberInStock: number;
  dailyRentalRate: number;
  imdbUrl: string;
  rating: number;
  genre: Genre;
  liked: boolean;
}
