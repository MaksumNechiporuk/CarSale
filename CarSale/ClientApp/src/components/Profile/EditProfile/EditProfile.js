import React, { Component } from 'react';

class EditProfile extends Component {

	render() {
		return (
			<div className="profile">
				<hr />
				<div className="container">
					<form
						enctype="multipart/form-data"
						className="form"

						method="post"
					>
						<div className="row">
							<div className="col-sm-3">
								<div className="text-center">
									<img
										src="http://ssl.gstatic.com/accounts/ui/avatar_2x.png"
										className="avatar img-circle img-thumbnail"
										alt="avatar"
									/>
									<h6>Upload a different photo...</h6>
									<input
										type="file"
										className="text-center center-block file-upload"
										accept=".png,.jpg,.jpeg,.gif,.tif"
									/>
								</div>
								<br />
							</div>

							<div className="col-sm-9">
								<div className="form-group">
									<div className="col-xs-6">
										<label ><h4>Email</h4></label>
										<input

											type="text"
											className="form-control"
											placeholder="Email"
											title="Enter your new Email"
										/>
										<span className="text-danger"></span>
									</div>
								</div>
								<div className="form-group">
									<div className="col-xs-6">
										<label ><h4>Password</h4></label>
										<input

											type="password"
											className="form-control"
											placeholder="Password"
											title="Enter your new Password"
										/>
										<span className="text-danger"></span>
									</div>
								</div>
								<div className="form-group">
									<div className="col-xs-6">
										<label ><h4>Confirm password</h4></label>
										<input
											type="password"
											className="form-control"
											placeholder="Confirm Password"
											title="Enter your Password"
										/>
										<span
											className="text-danger"
										></span>
									</div>
								</div>
								<div className="form-group">
									<div className="col-xs-6">
										<label><h4>City</h4></label>
										<input
											type="text"
											className="form-control"
											placeholder="City"
											title="Enter yourCity"
										/>
										<span className="text-danger"></span>
									</div>
								</div>

								<div className="form-group">
									<div className="col-xs-6">
										<label ><h4>Last name</h4></label>
										<input

											type="text"
											className="form-control"
											placeholder="Last name"
											title="Enter your Last name"
										/>
										<span className="text-danger"></span>
									</div>
								</div>
								<div className="form-group">
									<div className="col-xs-6">
										<label ><h4>Surname</h4></label>
										<input
											type="text"
											className="form-control"
											placeholder="Surname"
											title="Enter your Surname"
										/>
										<span className="text-danger"></span>
									</div>
								</div>
								<div className="text-danger"></div>

								<div className="form-group">
									<div className="col-xs-12">
										<br />
										<button
											className="btn_Save"
											type="submit"
										>
											<i className="fa fa-floppy-o" aria-hidden="true"></i>

                Save
                              </button>
										<button className="btn_Reset" type="reset">
											<i className="fa fa-refresh" aria-hidden="true"></i>
    Reset
              </button>
									</div>
								</div>
							</div>
						</div>
					</form>
				</div>
				<hr />
			</div>

		);
	}
}

export default EditProfile;