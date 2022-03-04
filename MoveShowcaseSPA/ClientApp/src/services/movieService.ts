import http from './httpService';
import { Movie } from '../interfaces/movie';

const apiEndpoint = `${process.env.REACT_APP_API_URL}movie`;

export const movieUrl = (id: number) => `${apiEndpoint}/Movie/${id}`;

export const getMovies = async () => http.get(`${apiEndpoint}/Movies`);

export const getMovie = async (id: number) => http.get(movieUrl(id));

export const toggleFavourite = async (id: number, rating: number) =>
  http.put(`${apiEndpoint}/ToggleFavourite`, { id, rating });

export const saveMovie = async (mov: Movie) =>
  http.post(`${apiEndpoint}/create`, { ...mov });

export const deleteMovie = async (movieId: number) =>
  http.delete(movieUrl(movieId));
