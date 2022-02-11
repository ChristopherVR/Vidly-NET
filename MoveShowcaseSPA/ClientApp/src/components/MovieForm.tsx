import React, { useEffect, useState } from 'react';
import Joi from 'joi-browser';
import Form from './common/formValidation';
import { getMovie, saveMovie } from '../services/movieService';
import { getGenres } from '../services/genreService';
import { Genre } from '../interfaces/genre';

function MovieForm() {
  const [genres, setGenres] = useState<Genre[]>([]);
  const [movie, setMovie] = useState<Movie>();
  const schema = {
    _id: Joi.string(),
    title: Joi.string().required().label('Title'),
    genreId: Joi.string().required().label('Genre'),
    numberInStock: Joi.number()
      .required()
      .min(0)
      .max(100)
      .label('Number in Stock'),
    dailyRentalRate: Joi.number()
      .required()
      .min(0)
      .max(10)
      .label('Daily Rental Rate'),
  };

  useEffect(() => {
    const populateGenres = async () => {
      const { data: gen } = await getGenres();
      setGenres(gen);
    };

    const populateMovie = async () => {
      try {
        const movieId = props.match.params.id;
        if (movieId === 'new') return;

        const { data } = await getMovie(movieId);
        setMovie(data);
      } catch (ex) {
        if (ex.response && ex.response.status === 404)
          history.replace('/not-found');
      }
    };
    populateGenres();
    populateMovie();
  }, []);

  const doSubmit = async () => {
    await saveMovie(this.state.data);

    props.history.push('/movies');
  };

  return (
    <div>
      <h1>Movie Form</h1>
      <form onSubmit={handleSubmit}>
        {renderInput('title', 'Title')}
        {renderSelect('genreId', 'Genre', genres)}
        {renderInput('numberInStock', 'Number in Stock', 'number')}
        {renderInput('dailyRentalRate', 'Rate')}
        {renderButton('Save')}
      </form>
    </div>
  );
}

export default MovieForm;
