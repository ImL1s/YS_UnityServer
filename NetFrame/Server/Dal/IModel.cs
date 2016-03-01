/*
 * Author:ImL1s
 *
 * Date:2016/03/01
 *
 * Email:ImL1s@outlook.com
 *
 * description:數據庫的模型介面.
 *
 */

public interface IModel
{
    /// <summary>
    /// 將當前模型Instance加入到資料庫中.
    /// </summary>
    /// <returns></returns>
    bool AddToDatabase();
}

