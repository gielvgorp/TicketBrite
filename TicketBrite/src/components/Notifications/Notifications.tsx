import { toast } from "react-toastify";

type Props = {
    text: string;
}

export function SuccessNotification({text}: Props){
    return toast.success(text, {
        position: "top-right",
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });
}

export function ErrorNotification({text}: Props){
    return toast.error(text, {
        position: "top-right",
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });
}