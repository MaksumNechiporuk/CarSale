import React, { Component, Fragment } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from "../../../../store/Actions/CarActions";
import { Lightbox } from 'primereact/lightbox';
import { TabView, TabPanel } from 'primereact/tabview';
import { ProgressSpinner } from 'primereact/progressspinner';
import AwesomeSlider from 'react-awesome-slider';
import 'react-awesome-slider/dist/styles.css';


class CarPost extends Component {
	constructor() {
		super();
		this.state = {
			images: null,
			mainImage: "",
			loading: true
		};
	}
	componentDidMount() {
		this.ensureDataFetched();
	}
	async	ensureDataFetched() {
		const id = this.props.match.params.id;
		await this.props.GetCarById(id);
		this.setState(({ mainImage, loading }) => {
			return {

				mainImage: this.props.selectCar.image[0],
				loading: false

			};

		});
	}
	render() {
		let { name, price } = this.props.selectCar;
		let { mainImage, loading } = this.state;
		var formatter = new Intl.NumberFormat('en-US', {
			style: 'currency',
			currency: 'USD',
		});
		price = formatter.format(price);
		let features, images, ImageForGallery;
		if (!loading) {
			features = this.props.selectCar.filters.map(item => {
				return (<li>{item.name}: {item.children.name}</li>)
			});
			images = this.props.selectCar.image.map(item => {
				return (<div className="image" data-src={item} />)
			});
			ImageForGallery = this.props.selectCar.image.map(item => {
				return ({
					source: `${item}`,
					thumbnail: `${item}`,
					title: 'Car image'
				})
			});
			console.log(ImageForGallery);
		}
		console.log(images);
		return (

			loading ? <div className="container"> <ProgressSpinner /></div> :

				<Fragment>
					<div className="container carPost">

						<div className="row">
							<div className="information_block">
								<div className="col-md-7">
									<AwesomeSlider bullets={false} fullScreen={true} className="image" showPlayButton={false}>
										{images}
									</AwesomeSlider>
								</div>
								<div className="col-md-5 informationCar">
									<h2 className="carName">{name}</h2>
									<p className="product-price">{price}</p>

								</div>
							</div>


						</div>
						<TabView>
							<TabPanel header="DESCRIPTION">
								<h3>{name} Features:</h3>
								<ul>
									{features}
								</ul>
							</TabPanel>
							<TabPanel header="Photo">
								<Lightbox className="imgGallery" type="images" images={ImageForGallery} />


							</TabPanel>
							<TabPanel header="Header III">
								Content III
								
							</TabPanel>
						</TabView>
					</div>
				</Fragment>
		);
	}
}
const mapStateToProps = state => {
	console.log(state);
	return {
		selectCar: state.carList.selectCar
	};
};

const mapDispatchToProps = dispatch => {
	const { GetCarById } = bindActionCreators(actionCreators, dispatch);
	return {
		GetCarById
	};
};

export default connect(mapStateToProps, mapDispatchToProps)(CarPost);