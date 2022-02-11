import Joi from 'joi-browser';

export const validateProperty = ({
  name,
  value,
}: {
  name: string;
  value: string;
}) => {
  const obj = { [name]: value };
  const schema = { [name]: this.schema[name] };
  const { error } = Joi.validate(obj, schema);
  return error ? error.details[0].message : null;
};

export const handleSubmit = (e: Event, doSubmit: () => void) => {
  e.preventDefault();

  const errors = validate();
  if (errors) return;
  doSubmit();
};

export const handleChange = ({ currentTarget: input }) => {
  const errors = { ...this.state.errors };
  const errorMessage = this.validateProperty(input);
  if (errorMessage) errors[input.name] = errorMessage;
  else delete errors[input.name];

  const data = { ...this.state.data };
  data[input.name] = input.value;

  return { errors, data };
};
