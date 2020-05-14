import React, { Component } from 'react';

class Registration extends Component {

    render() {
        return (
  

<body>
 <header>
 </header>
 <main>
  <div className="loginBox">
   <h2>Create your account!</h2>
  <form>
   <p>Name</p>
   <input type="text" name="name" placeholder="Name"></input>   
   <p>Surname</p>
   <input type="text" name="surname" placeholder="Surname"></input>   
   <p>Country</p>
   <input type="text" name="country" placeholder="Country"></input>  
   <p>City</p>
   <input type="text" name="city" placeholder="City"></input>  
   <p>E-mail Address</p>
   <input type="email" name="email" placeholder="Email"></input>   
   <p>Password</p>
   <input type="Password" name="pword1" placeholder="Create Password"></input>   
   <p>Re-enter Password</p>
   <input type="password" name="pword2" Placeholder="Confirm Password"></input>   
   <input type="Submit" name="sbmt"></input>                                                              
  <p>Already have an account? <a href="#">Sign In</a></p>   
  </form>
  </div>
 </main>
</body>

      
            );
    }
}

export default Registration;
