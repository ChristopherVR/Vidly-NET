import { faBasketball } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React from 'react';

function Loading() {
  return (
    <FontAwesomeIcon
      className="d-flex mx-auto position-absolute start-50 end-50 top-50"
      size="10x"
      icon={faBasketball}
      spin
    />
  );
}

export default Loading;
