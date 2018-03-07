 
 

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 2:00:25 PM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// ��ͼ��Ϊ��������
    /// </summary>
    public class AiukViewActionRequest<T> : IAiukViewActionRequest<T> where T : IAiukViewlement
    {
        public T Requester { get; private set; }

        public string Token { get; private set; }

        public void Init(T requester, string token)
        {
            Requester = requester;
            Token = token;
        }
    }
}
