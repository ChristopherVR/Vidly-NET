import React, { useContext, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { toast } from 'react-toastify';
import _ from 'lodash';
import MoviesTable from './MoviesTable';
import ListGroup from './common/ListGroup';
import Pagination from './common/Pagination';
import {
  getMovies,
  deleteMovie,
  toggleFavourite,
} from '../services/movieService';
import getGenres from '../services/genreService';
import paginate from '../utils/paginate';
import SearchBox from './SearchBox';
import { Genre } from '../interfaces/genre';
import { Movie } from '../interfaces/movie';
import UserContext from '../context/userContext';

function Movies() {
  const [movies, setMovies] = useState<Movie[]>([]);
  const [genres, setGenres] = useState<Genre[]>([]);
  const [criteria, setCriteria] = useState<{
    currentPage: number;
    pageSize: number;
    searchQuery: string;
    selectedGenre: number;
    sortColumn: {
      path: string;
      order: boolean | 'asc' | 'desc';
    };
  }>({
    currentPage: 1,
    pageSize: 4,
    searchQuery: '',
    selectedGenre: 0,
    sortColumn: {
      path: 'title',
      order: 'asc',
    },
  });

  useEffect(() => {
    const getData = async () => {
      const { data } = await getGenres();
      const allGenres = [{ id: 0, name: 'All Genres' }, ...data];
      setGenres(allGenres);
      const { data: mov } = await getMovies();
      setMovies(mov);
    };
    getData();
  }, []);

  const handleDelete = async (movie: Movie) => {
    const originalMovies = [...movies];
    const filteredMovies = originalMovies.filter((m) => m.id !== movie.id);
    setMovies(filteredMovies);

    try {
      if (!movie.id) throw new Error('Movie id is undefined');
      await deleteMovie(movie.id);
    } catch {
      toast.error('An error occured trying to delete the movie.');
      setMovies(originalMovies);
    }
  };

  const handleLike = async (movie: Movie) => {
    if (!movie.id) throw new Error('Movie does not contain an identifier');
    await toggleFavourite(movie.id, !movie.liked);
    setMovies((prevMovies) => {
      const clonedMovies = [...prevMovies];
      const index = clonedMovies.indexOf(movie);
      clonedMovies[index] = { ...clonedMovies[index], liked: !movie.liked };
      return clonedMovies;
    });
  };

  const handlePageChange = (currentPage: number) => {
    setCriteria((prevCriteria) => ({ ...prevCriteria, currentPage }));
  };

  const handleGenreSelect = (selectedGenre: number) => {
    setCriteria((prevCriteria) => ({ ...prevCriteria, selectedGenre }));
  };

  const handleSearch = (searchQuery: string) => {
    setCriteria((prevCriteria) => ({ ...prevCriteria, searchQuery }));
  };

  const handleSort = (sortColumn: { path: string }) => {
    setCriteria((prevCriteria) => ({
      ...prevCriteria,
      sortColumn: {
        ...prevCriteria.sortColumn,
        order: true,
        ...sortColumn,
      },
    }));
  };

  const { pageSize, currentPage, sortColumn, searchQuery, selectedGenre } =
    criteria;

  const getPagedData = () => {
    let filtered = movies;
    if (searchQuery)
      filtered = movies.filter((m) =>
        m.title.toLowerCase().startsWith(searchQuery.toLowerCase()),
      );
    if (selectedGenre !== 0)
      filtered = movies.filter((m) => m.genre.value === selectedGenre);

    const sorted = _.orderBy(filtered, [sortColumn.path], [sortColumn.order]);
    const paginatedMovies = paginate(sorted, currentPage, pageSize);

    return { totalCount: filtered.length, data: paginatedMovies as Movie[] };
  };

  const { length: count } = movies;
  const user = useContext(UserContext);

  if (count === 0) return <p>There are no movies in the database.</p>;

  const { totalCount, data: pagedMovies } = getPagedData();

  return (
    <div className="row">
      <div className="col-3">
        <ListGroup
          items={genres}
          selectedItem={criteria.selectedGenre}
          onItemSelect={handleGenreSelect}
          textProperty="name"
          valueProperty="id"
        />
      </div>
      <div className="col">
        {user && (
          <Link to="/movies/new" className="btn btn-primary mb-4">
            New Movie
          </Link>
        )}
        <p>Showing {totalCount} movies in the database.</p>
        <SearchBox value={searchQuery} onChange={handleSearch} />
        <MoviesTable
          movies={pagedMovies}
          onLike={handleLike}
          onDelete={handleDelete}
          onSort={handleSort}
        />
        <Pagination
          itemsCount={totalCount}
          pageSize={pageSize}
          currentPage={currentPage}
          onPageChange={handlePageChange}
        />
      </div>
    </div>
  );
}

export default Movies;
