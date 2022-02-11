import React, { useContext, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { toast } from 'react-toastify';
import _ from 'lodash';
import MoviesTable from './moviesTable';
import ListGroup from './common/listGroup';
import Pagination from './common/pagination';
import { getMovies, deleteMovie } from '../services/movieService';
import getGenres from '../services/genreService';
import paginate from '../utils/paginate';
import SearchBox from './searchBox';
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
    selectedGenre: number | undefined;
    sortColumn: {
      path: string;
      order: boolean | 'asc' | 'desc';
    };
  }>({
    currentPage: 1,
    pageSize: 4,
    searchQuery: '',
    selectedGenre: undefined,
    sortColumn: {
      path: 'title',
      order: 'asc',
    },
  });

  useEffect(() => {
    const getData = async () => {
      const { data } = await getGenres();
      const allGenres = [{ _id: '', name: 'All Genres' }, ...data];
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
      await deleteMovie(movie.id);
    } catch (ex) {
      if (ex.response && ex.response.status === 404)
        toast.error('This movie has already been deleted.');
      setMovies(originalMovies);
    }
  };

  const handleLike = (movie: Movie) => {
    const clonedMovies = [...movies];
    const index = clonedMovies.indexOf(movie);
    clonedMovies[index] = { ...clonedMovies[index] };
    setMovies(clonedMovies);
  };

  const handlePageChange = (pageSize: number) => {
    setCriteria({ ...criteria, pageSize });
  };

  const handleGenreSelect = (selectedGenre: number) => {
    setCriteria({ ...criteria, selectedGenre });
  };

  const handleSearch = (searchQuery: string) => {
    setCriteria({ ...criteria, searchQuery });
  };

  const handleSort = (sortColumn: {
    path: string;
    order: boolean | 'asc' | 'desc';
  }) => {
    setCriteria({ ...criteria, sortColumn });
  };

  const { pageSize, currentPage, sortColumn, searchQuery, selectedGenre } =
    criteria;

  const getPagedData = () => {
    let filtered = movies;
    if (searchQuery)
      filtered = movies.filter((m) =>
        m.title.toLowerCase().startsWith(searchQuery.toLowerCase()),
      );
    else if (selectedGenre)
      filtered = movies.filter((m) => m.genre.id === selectedGenre);

    const sorted = _.orderBy(filtered, [sortColumn.path], [sortColumn.order]);

    const paginatedMovies = paginate(sorted, currentPage, pageSize);

    return { totalCount: filtered.length, data: paginatedMovies };
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
        />
      </div>
      <div className="col">
        {user && (
          <Link
            to="/movies/new"
            className="btn btn-primary"
            style={{ marginBottom: 20 }}
          >
            New Movie
          </Link>
        )}
        <p>Showing {totalCount} movies in the database.</p>
        <SearchBox value={searchQuery} onChange={handleSearch} />
        <MoviesTable
          movies={pagedMovies}
          sortColumn={sortColumn}
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
