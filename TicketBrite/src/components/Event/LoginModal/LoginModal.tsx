import LoginForm from '../../Authentication/LoginForm'
import './LoginModal.css'

function LoginModal(){
    return (
        <>
            <div className="main-modal-bg">
                <div className="position-absolute main-modal shadow">
                    <div className="modal-bottom bg-secondary d-flex align-items-center p-3">
                        <h3 className='font text-white'>Je bent nog niet ingelogd!</h3>
                    </div>
                    <div className="modal-content p-5">
                        <LoginForm />
                    </div>
                    <div className="modal-bottom bg-secondary d-flex justify-content-end align-items-center px-4 gap-3">
                        <button className="btn btn-danger py-2 px-3">Doorgaan zonder account</button>
                    </div>
                </div>
            </div>
        </>
    )
}

export default LoginModal