import http from './httpService';

const getGenres = (searchTerm?: string) =>
  http.get(`${process.env.REACT_APP_API_URL}/genres`, {
    data: { searchTerm },
  });

export default getGenres;
