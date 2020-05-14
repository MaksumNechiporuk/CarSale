import React, { Component } from 'react';

class ShowProfile extends Component {

	render() {
		return (
			<div className="profile">
				<div className="container portfolio">
					<div className="row">
						<div className="col-md-12">
							<div className="heading">
								<img src="https://image.ibb.co/cbCMvA/logo.png" />
							</div>
						</div>
					</div>
					<div className="bio-info">
						<div className="row">
							<div className="col-md-6">
								<div className="row">
									<div className="col-md-12">
										<div className="bio-image">
											<img
												src="http://ssl.gstatic.com/accounts/ui/avatar_2x.png"
												className="avatar img-circle img-thumbnail"
												alt="avatar"
											/>
										</div>
									</div>
								</div>
							</div>
							<div className="col-md-6">
								<div className="bio-content">
									<h1>@Model.Email</h1>
									<div className="col-xs-6">
										<label><h6>First name:</h6></label>
										<label><h6>@Model.First name</h6></label>
									</div>
									<div className="col-xs-6">
										<label><h6>Last name:</h6></label>
										<label><h6>@Model.last name</h6></label>
									</div>
									<div className="col-xs-6">
										<label><h6>City:</h6></label>
										<label><h6>@Model.City</h6></label>
									</div>

									<button asp-action="Edit" asp-controller="Profile" className="btn_Edit"
									><i className="fa fa-pencil" aria-hidden="true"></i> Edit</button
									>
									<button asp-action="Edit" asp-controller="Profile" className="btn_SignOut"
									><i className="fa fa-sign-out" aria-hidden="true"></i>
 Sign out</button
									>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>

		);
	}
}

export default ShowProfile;