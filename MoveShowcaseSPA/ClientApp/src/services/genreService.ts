import http from './httpService';
import apiUrl from '../config.json';

const getGenres = () => http.get(`${apiUrl}/genres`);

export default getGenres;
