import http from './httpService';
import apiUrl from '../config.json';

const getGenres = (searchTerm?: string) =>
  http.get(`${apiUrl}/genres`, {
    data: { searchTerm },
  });

export default getGenres;
