import http from './httpService';
import { Movie } from '../interfaces/movie';

const apiEndpoint = `${process.env.REACT_APP_API_URL}movies`;

export const movieUrl = (id: number) => `${apiEndpoint}/Movie/${id}`;

export const getMovies = async () => http.get(`${apiEndpoint}/`);

export const getMovie = async (id: number) => http.get(movieUrl(id));

export const toggleFavourite = async (id: number, rating: number) =>
  http.put(`${apiEndpoint}/movie/ToggleFavourite`, { id, rating });

export const saveMovie = async (mov: Movie) =>
  http.post(`${apiEndpoint}/movie/create`, { ...mov, genre: mov.genre.id });

export const deleteMovie = async (movieId: number) =>
  http.delete(movieUrl(movieId));
