import http from './httpService';
import { Movie } from '../interfaces/movie';

const apiEndpoint = `${process.env.REACT_APP_API_URL}movies`;

export const movieUrl = (id: number) => `${apiEndpoint}/Movie/${id}`;

export const getMovies = async () => http.get(`${apiEndpoint}/`);

export const getMovie = async (id: number) => http.get(movieUrl(id));

export const toggleFavourite = async (id: number, rating: number) =>
  http.put(`${apiEndpoint}/movie/ToggleFavourite`, { id, rating });

export const saveMovie = async (mov: Movie) => {
  if (mov.id) {
    await http.patch(`${apiEndpoint}/movie/${mov.id}`, {
      liked: mov.liked,
      dailyRentalRate: Number(mov.dailyRentalRate),
      numberInStock: Number(mov.numberInStock),
      title: mov.title,
      rating: Number(mov.rating),
      imdbUrl: mov.imdbUrl,
      genreId: mov.genre.value,
    });
  } else {
    await http.post(`${apiEndpoint}/movie/create`, {
      liked: mov.liked,
      dailyRentalRate: Number(mov.dailyRentalRate),
      numberInStock: Number(mov.numberInStock),
      title: mov.title,
      rating: Number(mov.rating),
      imdbUrl: mov.imdbUrl,
      genreId: mov.genre.value,
    });
  }
};

export const deleteMovie = async (movieId: number) =>
  http.delete(movieUrl(movieId));
