import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { MDBValidation, MDBInput, MDBBtn, MDBSwitch } from 'mdb-react-ui-kit';
import { toast } from 'react-toastify';
import { AsyncSelect } from 'react-select/async';
import { SingleValue } from 'react-select';
import { getMovie, saveMovie } from '../services/movieService';
import { Genre } from '../interfaces/genre';
import { Movie } from '../interfaces/movie';
import getGenres from '../services/genreService';

type ParamsProps = {
  id: string;
};

function MovieForm() {
  const [genres, setGenres] = useState<Genre[]>([]);
  const [movie, setMovie] = useState<Movie>({
    dailyRentalRate: 0,
    genre: {
      id: 0,
      name: '',
    },
    liked: false,
    numberInStock: 0,
    rating: 0,
    title: '',
    imdbUrl: '',
  });
  const navigate = useNavigate();
  const { id } = useParams<ParamsProps>();

  useEffect(() => {
    const populateGenres = async () => {
      try {
        const { data: gen } = await getGenres();
        setGenres(gen);
      } catch {
        toast.error('An error occurred trying to retrieve movie genres.');
      }
    };

    const populateMovie = async () => {
      try {
        if (id === 'new') return;

        const { data } = await getMovie(Number(id));
        setMovie(data);
      } catch {
        navigate('/not-found');
      }
    };
    populateGenres();
    populateMovie();
  }, [id, navigate]);

  const onChangeHandler = (ev: HTMLInputElement) => {
    setMovie((prevMovie: Movie) => ({
      ...prevMovie,
      [ev.name]: ev.value,
    }));
  };

  const onChange = (ev: Genre) => {
    setMovie((prevMovie: Movie) => ({
      ...prevMovie,
      genre: ev,
    }));
  };

  const doSubmit = async () => {
    await saveMovie(movie);
    navigate('/movies');
  };

  return (
    <div>
      <h1>Movie Form</h1>
      <MDBValidation id="movie-form" onSubmit={doSubmit}>
        <MDBInput
          name="dailyRentalRate"
          label="Daily Rental Rate"
          type="text"
          value={movie.dailyRentalRate.toString()}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Daily Rental Rate is required"
        />
        <MDBInput
          name="rating"
          label="Rating"
          type="text"
          value={movie.rating.toString()}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Rating is required"
        />
        <MDBInput
          name="title"
          label="Title"
          type="text"
          value={movie.title}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Title is required"
        />
        <MDBInput
          name="numberInStock"
          label="Number in Stock"
          type="text"
          value={movie.numberInStock.toString()}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Number in Stock is required"
        />
        <MDBInput
          name="imdbUrl"
          label="IMDB Url"
          type="url"
          value={movie.imdbUrl}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
        />
        <MDBSwitch
          value={movie.liked}
          name="liked"
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          label="Liked"
        />
        <AsyncSelect
          className="basic-single"
          classNamePrefix="select"
          defaultValue={genres[0]}
          isClearable={false}
          isSearchable
          defaultOptions={genres}
          onChange={(ev: SingleValue<Genre>) => {
            onChange(ev as Genre);
          }}
          value={movie.genre}
          name="genre"
        />
        {/* Add Genre */}
        <MDBBtn form="movie-form" type="submit">
          Submit
        </MDBBtn>
      </MDBValidation>
    </div>
  );
}

export default MovieForm;
