import React from 'react';

const CarItem = (props) => {
	let { name, state, year, img, price, mileage } = props;
	var formatter = new Intl.NumberFormat('en-US', {
		style: 'currency',
		currency: 'USD',
	});
	price = formatter.format(price); /* $2,500.00 */

	return (
		<div className="col-xl-4 col-sm-12 col-md-6 ">
			<div className="card rounded">
				<div className="card-image">
					<span className="card-notify-year">{year}</span>
					<img className="img-fluid " src={img} alt="Alternate Text" />
				</div>
				<div className="card-image-overlay m-auto">
					<span className="card-detail-badge">{state}</span>
					<span className="card-detail-badge">{price}</span>
					<span className="card-detail-badge">{mileage} Kms</span>
				</div>
				<div className="card-body text-center">
					<div className="m-auto">
						<h5>{name}</h5>
					</div>

				</div>
			</div>
		</div>);

}

export default CarItem;